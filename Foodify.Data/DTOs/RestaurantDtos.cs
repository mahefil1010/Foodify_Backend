public class CreateRestaurantDto
{
    public string Name { get; set; }
    public string Address { get; set; }
    public Guid OwnerId { get; set; }
}

public class UpdateRestaurantDto
{
    public string? Name { get; set; }
    public string? Address { get; set; }
    public Guid? OwnerId { get; set; }
}

public class RestaurantResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public Guid OwnerId { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid? ModifiedBy { get; set; }
}
