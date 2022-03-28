namespace TryFirstWorkApi.Testing
{
    public interface IValidateWireTransfer
    {
        OperationResult validate(Account origin, Account destination, decimal amount);
    }
}
