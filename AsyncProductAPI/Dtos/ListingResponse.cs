namespace AsyncProductAPI.Dtos
{
    public class ListingResponse
    {
        public DateTime EstimatedCompletionTime { get; set; }
        public string? RequestStatus { get; set; }
        public Guid RequestId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Resource URL that returns the created product back
        /// </summary>
        public Uri? FinalEndpoint { get; set; }
    }
}
