public class CreateDeliveryAssignmentDto
{
    public Guid OrderId { get; set; }
    public Guid DeliveryPartnerId { get; set; }
    public DateTime AssignmentTime { get; set; }
}

public class UpdateDeliveryAssignmentDto
{
    public DateTime? DeliveryTime { get; set; }
}

public class DeliveryAssignmentResponseDto
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid DeliveryPartnerId { get; set; }
    public DateTime AssignmentTime { get; set; }
    public DateTime? DeliveryTime { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid? ModifiedBy { get; set; }
}
