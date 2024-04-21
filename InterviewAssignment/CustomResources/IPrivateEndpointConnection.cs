namespace InterviewAssignmnet.CustomResources;

public interface IPrivateEndpointConnection
{
    public Pulumi.Output<string> Id { get; }
}
