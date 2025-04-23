﻿using Microsoft.AspNetCore.Authorization;

namespace OWT.BoatManager.Api.Security;

internal sealed class HasScopeHandler : AuthorizationHandler<HasScopeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasScopeRequirement requirement)
    {
        if (!context.User.HasClaim(c => c.Type == "scope" && c.Issuer == requirement.Issuer))
            return Task.CompletedTask;

        var scopes = context.User.FindFirst(c => c.Type == "scope" && c.Issuer == requirement.Issuer)?.Value.Split(' ');

        if (scopes?.Any(s => s == requirement.Scope) ?? false)
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}