import ChangeCircleIcon from '@mui/icons-material/ChangeCircle';
import HighQualityIcon from '@mui/icons-material/HighQuality';
import SupportAgentIcon from '@mui/icons-material/SupportAgent';

const Policy = () => {
  return (
    <div className='flex flex-col sm:flex-row justify-around gap-12 sm:gap-2 text-center py-20 text-xs sm:text-sm md:text-base text-gray-700'>
      <div>
        <ChangeCircleIcon />
        <p className='font-semibold'>Easy Exchange policy</p>
      </div>
      <div>
        <HighQualityIcon />
        <p className='font-semibold'>7 Days Return Policy</p>
      </div>
      <div>
        <SupportAgentIcon />
        <p className='font-semibold'>Best customer support</p>
      </div>
    </div>
  );
};

export default Policy;
