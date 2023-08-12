import {useEffect, useState} from "react";
import {ping} from "../../api/controllers/TestClient";

const Root = () => {

    useEffect(() => {
        ping().then((response) => console.log(response));
    }, [])

    return (
        <div>
            Root
        </div>
    );
};

export default Root;
