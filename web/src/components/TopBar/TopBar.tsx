import React from 'react';
import {AppBar, Box, Toolbar, Typography} from "@mui/material";
import {Link} from "react-router-dom";

const TopBar = () => {
    return (
        <Box sx={{ flexGrow: 1 }}>
            <AppBar position="static">
                <Toolbar>
                    <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                        Tesla Rent
                    </Typography>
                    <Link to={'/login'}>Login</Link>
                    <Link to={'/register'}>Register</Link>
                    <Link to={'/reservations'}>Your reservations</Link>
                </Toolbar>
            </AppBar>
        </Box>
    );
};

export default TopBar;
