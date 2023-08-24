import { createTheme } from '@mui/material';
import {green, red} from "@mui/material/colors";

export const theme = createTheme({
  palette: {
    primary: {
      main: '#28282a',
    },
    secondary: {
      main: '#ff3366',
    },
    warning: {
      main: '#ffc071',
    },
    error: {
      main: red[500],
    },
    success: {
      main: green[500],
    },
  },
  spacing: 8,
  shape: {
    borderRadius: 4,
  }
});
