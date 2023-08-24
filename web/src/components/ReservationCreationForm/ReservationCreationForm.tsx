import React, { useState } from 'react';
import StepOne from './StepOne';
import StepTwo from './StepTwo';
import {CreateReservationRequest} from "../../api/models/Reservations/CreateReservationRequest";
import dayjs  from "dayjs";
import utc from "dayjs/plugin/utc";
import {useSnackbar} from "notistack";
import {createReservation} from "../../api/controllers/ReservationsClient";
import {useNavigate} from "react-router-dom";
import {userId} from "../../constants/User";

dayjs.extend(utc);

const ReservationCreationForm: React.FC = () => {
    const [step, setStep] = useState<number>(1);
    const [formData, setFormData] = useState<CreateReservationRequest>({
        startDate: dayjs.utc().toJSON(),
        endDate: dayjs.utc().toJSON(),
        startLocationId: 1,
        endLocationId: 1,
        userId: userId,
        carId: 0
    });

    const navigate = useNavigate();
    const { enqueueSnackbar } = useSnackbar();

    const handleNext = () => {
        setStep(step + 1);
    };

    const handleBack = () => {
        setStep(step - 1);
    };

    const handleSubmit = async () => {
        if (validate()){
            await createReservation(formData).then((reservation) => {
                enqueueSnackbar("Reservation created", { variant: 'success' });
                navigate(`/reservations/${reservation.id}`);
            }).catch(() =>{
                enqueueSnackbar("Something went wrong", { variant: 'error' });
            })
        }
        console.log('Form submitted:', formData);
    };

    const validate = () :boolean => {
        if (formData.startLocationId == 0){
            enqueueSnackbar("Please select start location", { variant: 'error' });
            return false;
        }

        if (formData.endLocationId == 0){
            enqueueSnackbar("Please select end location", { variant: 'error' });
            return false;
        }

        if (formData.carId == 0){
            enqueueSnackbar("Please select end car", { variant: 'error' });
            return false;
        }

        return true;
    }

    return (
        <div>
            {step === 1 && (
                <StepOne formData={formData} setFormData={setFormData} onNext={handleNext} />
            )}
            {step === 2 && (
                <StepTwo formData={formData} setFormData={setFormData} onBack={handleBack}  onSubmit={handleSubmit} />
            )}
        </div>
    );
};

export default ReservationCreationForm;
