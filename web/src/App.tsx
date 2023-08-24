import React from 'react';
import {Route, Routes} from "react-router-dom";
import Root from "./routes/Root/Root";
import Reservations from "./routes/Resrvations/Reservations";
import ReservationDetails from "./routes/ReservationDetails/ReservationDetails";
import CreateReservation from "./routes/CreateReservation/CreateReservation";
import LandingPage from "./routes/LandingPage/LandingPage";
import {AuthenticationGuard} from "./routes/AuthenticationGuard";

const App = () => {
    return (
        <Routes>
            <Route path="/landing" element={<LandingPage />} />
            <Route path="/" element={<Root />} />
            <Route
                path="/createReservation"
                element={<AuthenticationGuard component={CreateReservation} />}
            />
            <Route
                path="/reservations"
                element={<AuthenticationGuard component={Reservations} />}
            />
            <Route
                path="/reservations/:reservationId"
                element={<AuthenticationGuard component={ReservationDetails} />}
            />
        </Routes>
    );
};

export default App;
