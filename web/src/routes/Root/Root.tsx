import React, {useEffect} from 'react';
import {useNavigate} from "react-router-dom";

const Root = () => {

    const navigate = useNavigate();

    useEffect(() => {
        navigate("/createReservation")
    }, []);


    return (
        <div>
            Root
        </div>
    );
};

export default Root;
