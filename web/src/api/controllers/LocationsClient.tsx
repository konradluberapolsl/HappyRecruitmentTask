import Client from "../Client";
import {LocationDto} from "../models/Locations/LocationService";

const controllerName = "Locations";

const getLocations = async () : Promise<LocationDto> => {
    return (await Client.get(`${controllerName}`)).data;
}

export { getLocations }
