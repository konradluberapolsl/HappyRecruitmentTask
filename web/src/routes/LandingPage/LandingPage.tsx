import React from 'react';
import {Box, Button, Container, Stack, Typography} from "@mui/material";
import LoginButton from "../../components/Buttons/LoginButton";
import {useNavigate} from "react-router-dom";

const LandingPage = () => {
    const navigate = useNavigate();
    return (
        <main>
            <Box
                display="flex"
                justifyContent="center"
                alignItems="center"
                minHeight="100vh"
                component="div"
            >
                <Container sx={{ height: '100v' }} >
                    <Typography
                        component="h1"
                        variant="h2"
                        align="center"
                        color="text.primary"
                        gutterBottom
                    >
                        Rent your dream Tesla!
                    </Typography>
                    <Typography variant="h5" align="center" color="text.secondary" paragraph>
                        Join us and make your automotive dreams come true.
                    </Typography>
                    <Stack
                        sx={{ pt: 4 }}
                        direction="row"
                        spacing={2}
                        justifyContent="center"
                    >
                        <LoginButton />
                    </Stack>
                </Container>
            </Box>
        </main>
    );
};

export default LandingPage;
