namespace TopBookStore.Domain.Constants;

public static class StatusConstants
{
    public const string StatusPending = "Pending";
    public const string StatusApproved = "Approved";
    public const string StatusInProcess = "Processing";
    public const string StatusShipped = "Shipped";
    public const string StatusCancelled = "Cancelled";
    public const string StatusRefunded = "Refunded";

    public const string PaymentStatusPending = "Pending";
    public const string PaymentStatusApproved = "Approved";
    public const string PaymentStatusDelayed = "ApprovedForDelayedPayment";
    public const string PaymentStatusRejected = "Rejectedd";
}
