public class CreateDeliveryPartnerDto
{
    public Guid UserId { get; set; }
    public string? VehicleNumber { get; set; }
    public string? CurrentStatus { get; set; }
}

public class UpdateDeliveryPartnerDto
{
    public string? VehicleNumber { get; set; }
    public string? CurrentStatus { get; set; }
}

public class DeliveryPartnerResponseDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? VehicleNumber { get; set; }
    public string? CurrentStatus { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid? ModifiedBy { get; set; }
}
