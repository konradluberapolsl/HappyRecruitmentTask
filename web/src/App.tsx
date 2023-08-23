import React from 'react';
import TopBar from "./components/TopBar/TopBar";
import {BrowserRouter, Route, Routes} from "react-router-dom";
import Root from "./routes/Root/Root";
import Login from "./routes/Login/Login";
import Register from "./routes/Register/Register";
import Reservations from "./routes/Resrvations/Reservations";
import ReservationDetails from "./routes/ReservationDetails/ReservationDetails";

const App = () => {
    return (
        <BrowserRouter>
            <TopBar/>
            <Routes>
                <Route path="/" element={<Root />} />
                <Route path="/login" element={<Login />} />
                <Route path="/register" element={<Register />} />
                <Route path="/reservations" element={<Reservations />} />
                <Route path="/reservations/:reservationId" element={<ReservationDetails />} />
            </Routes>
        </BrowserRouter>
    );
};

export default App;
