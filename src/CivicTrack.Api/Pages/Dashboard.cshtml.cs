using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CivicTrack.Data.Contexts;
using CivicTrack.Data.Entities;

public class DashboardModel : PageModel
{
    private readonly AppDbContext _context;

    public DashboardModel(AppDbContext context)
    {
        _context = context;
    }

    // Current user info
    public User? CurrentUser { get; set; }

    // List of service requests to display
    public List<ServiceRequest> Requests { get; set; } = new();

    // For creating new service requests
    [BindProperty]
    public ServiceRequest? NewRequest { get; set; }

    // Optional: allow role override for prototype testing
    [BindProperty(SupportsGet = true)]
    public string? Role { get; set; }

    public async Task OnGetAsync(int userId)
    {
        // Get the user from DB
        CurrentUser = await _context.Users.FindAsync(userId);

        // If a role query parameter is provided, override the user's role
        if (!string.IsNullOrEmpty(Role))
        {
            CurrentUser!.Role = Role;
        }

        // Load requests based on role
        if (CurrentUser?.Role == "Admin")
        {
            // Admin sees all requests
            Requests = await _context.ServiceRequests
                .Include(r => r.CreatedByUser)
                .Include(r => r.AssignedWorker)
                .ToListAsync();
        }
        else if (CurrentUser?.Role == "CivilWorker")
        {
            // CivilWorker sees requests assigned to them or pending
            Requests = await _context.ServiceRequests
                .Where(r => r.AssignedWorkerId == userId || r.Status == "Pending")
                .Include(r => r.CreatedByUser)
                .ToListAsync();
        }
        else if (CurrentUser?.Role == "User")
        {
            // User sees only their own requests
            Requests = await _context.ServiceRequests
                .Where(r => r.CreatedByUserId == userId)
                .Include(r => r.AssignedWorker)
                .ToListAsync();
        }
    }

    public async Task<IActionResult> OnPostAsync(int userId)
    {
        CurrentUser = await _context.Users.FindAsync(userId);

        if (CurrentUser?.Role != "User")
            return RedirectToPage(new { userId });

        if (NewRequest != null)
        {
            NewRequest.CreatedByUserId = userId;
            NewRequest.Status = "Pending";

            _context.ServiceRequests.Add(NewRequest);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage(new { userId });
    }
}