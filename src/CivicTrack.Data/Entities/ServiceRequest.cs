namespace CivicTrack.Data.Entities
{
    public class ServiceRequest
    {
        public int? Id { get; set; }

        public string? Title { get; set; }
        public string? Description { get; set; }

        public int? CreatedByUserId { get; set; }
        public User? CreatedByUser { get; set; }

        public string Status { get; set; } = "Pending"; 
        // "Pending", "Approved", "Assigned", "Completed"

        public int? AssignedWorkerId { get; set; }
        public User? AssignedWorker { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}