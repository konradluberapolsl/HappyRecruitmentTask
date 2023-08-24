import React from 'react';
import TopBar from "./components/TopBar/TopBar";
import {BrowserRouter, Route, Routes} from "react-router-dom";
import Root from "./routes/Root/Root";
import Reservations from "./routes/Resrvations/Reservations";
import ReservationDetails from "./routes/ReservationDetails/ReservationDetails";
import CreateReservation from "./routes/CreateReservation/CreateReservation";

const App = () => {
    return (
        <BrowserRouter>
            <TopBar/>
            <Routes>
                <Route path="/" element={<Root />} />
                <Route path="/createReservation" element={<CreateReservation />} />
                <Route path="/reservations" element={<Reservations />} />
                <Route path="/reservations/:reservationId" element={<ReservationDetails />} />
            </Routes>
        </BrowserRouter>
    );
};

export default App;
