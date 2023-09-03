import Client from "../Client"
import {DateTimeRangeDto} from "../models/Availability/DateTimeRangeDto";
import {CarsVm} from "../models/Cars/CarsVm";

const controllerName = "Availability";

const getAvailableCarsByLocationAndTimeRange = async (locationId: number, startDate: string, endDate: string): Promise<CarsVm> => {
    return (await Client.get(`${controllerName}/cars`, { params:
            {
                locationId: locationId,
                startDate: startDate,
                endDate: endDate
            } })).data;
};

const getUnavailableRentalDatesForUser =  async () : Promise<DateTimeRangeDto[]> => {
    return (await  Client.get(`${controllerName}/user/unavailableRentalDates`)).data;
};


export { getUnavailableRentalDatesForUser, getAvailableCarsByLocationAndTimeRange }
