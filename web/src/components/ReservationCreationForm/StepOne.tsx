import React, {useCallback, useEffect, useState} from 'react';
import {Button, Typography, Grid, Select, MenuItem, SelectChangeEvent} from '@mui/material';
import {CreateReservationRequest} from "../../api/models/Reservations/CreateReservationRequest";
import {DateRangePicker} from "@mui/x-date-pickers-pro/DateRangePicker";
import {DateRange} from "@mui/x-date-pickers-pro";
import dayjs, {Dayjs} from "dayjs";
import utc from "dayjs/plugin/utc";
import {DateTimeRangeDto} from "../../api/models/Availability/DateTimeRangeDto";
import {LocationDto} from "../../api/models/Locations/LocationDto";
import {getUnavailableRentalDatesForUser} from "../../api/controllers/AvailabilityClient";
import {getLocations} from "../../api/controllers/LocationsClient";
import {useSnackbar} from "notistack";
import {userId} from "../../constants/User";
import PageLoader from "../../routes/PageLoader";

dayjs.extend(utc);

interface StepOneProps {
    formData: CreateReservationRequest;
    setFormData: React.Dispatch<React.SetStateAction<CreateReservationRequest>>;
    onNext: () => void;
}

const tomorrow = dayjs().add(1, 'day');
const StepOne: React.FC<StepOneProps> = ({ formData, setFormData, onNext }) => {
    const [unavailableDates, setUnavailableDates] = useState<DateTimeRangeDto[]>([])
    const [locations, setLocations] = useState<LocationDto[]>([]);
    const [dateRange, setDateRange] = useState<DateRange<Dayjs>>([
        dayjs.utc(formData.startDate),
        dayjs.utc(formData.startDate)
    ]);
    const [startLocationId, setStartLocationId] = useState('1');
    const [endLocationId, setEndLocationId] = useState('1');
    const [isLoading, setIsLoading] = useState<boolean>(true)

    const { enqueueSnackbar } = useSnackbar();

    const fetchData = useCallback(() => {
        getLocations().then((locations) =>{
            setLocations(locations);
            setIsLoading(false);
        }).catch(() =>{
            enqueueSnackbar("Something went wrong", { variant: 'error' });
        });

        getUnavailableRentalDatesForUser(userId).then((unavailableDates) =>{
            setUnavailableDates(unavailableDates);
        }).catch(() => {
            enqueueSnackbar("Something went wrong", { variant: 'error' });
        });
    }, []);

    useEffect(() => {
        fetchData();
    }, [fetchData]);


    const isInRange = (date: Dayjs) : boolean => {
        for (const unavailableDate of unavailableDates){
            const start = dayjs.utc(unavailableDate.startDate);
            const end = dayjs.utc(unavailableDate.endDate);

            if (date >= start && date <= end){
                return true;
            }
        }
        return false;
    }

    const handleDateRangeChange = (dateRange: DateRange<Dayjs>) => {
        setFormData({
            ...formData,
            startDate: dayjs(dateRange[0]).toJSON() ?? '',
            endDate: dayjs(dateRange[1]).toJSON() ?? ''
        })

        setDateRange(dateRange);
    }

    const handleNextClicked = () =>{

        if (formData.startLocationId == 0 || formData.endLocationId == 0){
            enqueueSnackbar("Please select locations", { variant: 'error' });
        }
        else {
            onNext();
        }
    }

    const handleChangeStartLocation = (event: SelectChangeEvent) => {
        const newValue = event.target.value;
        setStartLocationId(newValue);
        setFormData({ ...formData, startLocationId: parseInt(newValue) });
    };

    const handleChangeEndLocation = (event: SelectChangeEvent) => {
        const newValue = event.target.value;
        setEndLocationId(newValue);
        setFormData({ ...formData, endLocationId: parseInt(newValue) });
    };

    if (isLoading){
        return <PageLoader/>;
    }

    return (
        <Grid container
              direction="column"
              justifyContent="center"
              alignItems="center"
              spacing={3}>

            <Grid></Grid>

            <Grid item xs={12}>
            <Typography sx={{mb: 2}} variant='h6'>Choose date:</Typography>
            <DateRangePicker
                disablePast
                minDate={tomorrow}
                localeText={{ start: 'Start', end: 'End' }}
                value={dateRange}
                onChange={handleDateRangeChange}
                shouldDisableDate={isInRange}
                sx={{mb: 4}}
            />
            </Grid>

            <Grid
                item
                xs={12}
                container
                direction="row"
                justifyContent="center"
                alignItems="center"
                spacing={5}
            >
                <Grid item>
                    <Typography sx={{mb: 2}} variant='h6'>Choose start location:</Typography>
                    <Select
                        value={startLocationId}
                        onChange={handleChangeStartLocation}
                    >
                        {locations.map((option) => (
                            <MenuItem key={option.id} value={option.id}>
                                {option.name}
                            </MenuItem>
                        ))}
                    </Select>
                </Grid>
                <Grid item>
                    <Typography sx={{mb: 2}} variant='h6'>Choose end location:</Typography>
                    <Select
                        value={endLocationId}
                        onChange={handleChangeEndLocation}
                    >
                        {locations.map((option) => (
                            <MenuItem key={option.id} value={option.id}>
                                {option.name}
                            </MenuItem>
                        ))}
                    </Select>
                </Grid>

            </Grid>

            <Grid item xs={12}>
                <Button variant="contained" color="primary" onClick={handleNextClicked}>
                    Next
                </Button>
            </Grid>
        </Grid>
    );
};

export default StepOne;
