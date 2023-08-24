import React from "react";
import { AppState, Auth0Provider } from "@auth0/auth0-react";
import { useNavigate } from "react-router-dom";
import config from "../config.json";

type Auth0ProviderWithNavigateProps = {
    children: React.ReactNode;
};

export const Auth0ProviderWithNavigate = ({
                                              children,
                                          }: Auth0ProviderWithNavigateProps) => {
    const navigate = useNavigate();

    const domain = config.Auth0.Domain;
    const clientId = config.Auth0.ClientId;
    const redirectUri = window.location.origin;

    const onRedirectCallback = (appState: AppState | undefined) => {
        navigate(appState?.returnTo || window.location.pathname);
    };

    if (!(domain && clientId && redirectUri)) {
        return null;
    }

    return (
        <Auth0Provider
            domain={domain}
        clientId={clientId}
        authorizationParams={{
            redirect_uri: redirectUri,
            audience: config.Auth0.Audience,
        }}
        onRedirectCallback={onRedirectCallback}
         useRefreshTokens={true}
        >
            {children}
        </Auth0Provider>
);
};
