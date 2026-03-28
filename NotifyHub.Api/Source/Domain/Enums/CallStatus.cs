namespace NotifyHub.Api.Source.Domain.Enums
{
    public enum CallType
    {
        Police = 0,      // IsPolice = true
        Fire = 1,        // IsFire = true
        EMS = 2,         // IsEMS = true (Medical/Ambulance)
        Multiple = 3     // Combination of above
    }
    public enum CallStatus
    {
        New = 0,          // Just created
        InProgress = 1,   // Being handled
        Finished = 2      // Completed
    }
}
