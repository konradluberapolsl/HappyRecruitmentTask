import React from "react";
import { withAuthenticationRequired } from "@auth0/auth0-react";
import PageLoader from "./PageLoader";

export const AuthenticationGuard = ({ component }: any) => {
    const Component = withAuthenticationRequired(component, {
        onRedirecting: () => (
            <div>
                <PageLoader />
            </div>
        ),
    });

    return <Component />;
};
