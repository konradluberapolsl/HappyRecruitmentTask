import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.scss'
import reportWebVitals from './reportWebVitals';
import App from "./App";
import {ThemeProvider} from "@mui/material";
import {theme} from "./constants/ThemeMUI";
import {LocalizationProvider} from "@mui/x-date-pickers";
import {AdapterDayjs} from "@mui/x-date-pickers/AdapterDayjs";
import { SnackbarProvider } from 'notistack';
import {BrowserRouter} from "react-router-dom";
import TopBar from "./components/TopBar/TopBar";
import {Auth0ProviderWithNavigate} from "./auth/Auth0ProviderWithNavigate";

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);
root.render(
  <React.StrictMode>
      <BrowserRouter>
          <Auth0ProviderWithNavigate>
              <ThemeProvider theme={theme}>
                  <LocalizationProvider dateAdapter={AdapterDayjs}>
                      <SnackbarProvider
                          autoHideDuration={1500}
                          maxSnack={2}
                          anchorOrigin={{ horizontal: 'center', vertical: 'bottom' }}
                      >
                          <TopBar/>
                          <App/>
                      </SnackbarProvider>
                  </LocalizationProvider>
              </ThemeProvider>
          </Auth0ProviderWithNavigate>
      </BrowserRouter>
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
