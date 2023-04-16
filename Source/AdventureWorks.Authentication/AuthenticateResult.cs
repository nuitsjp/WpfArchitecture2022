namespace AdventureWorks.Authentication;

public record AuthenticateResult(bool IsAuthenticated, IAuthenticationContext Context);