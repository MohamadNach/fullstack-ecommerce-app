import { Link } from 'react-router-dom';
import { assets } from '../assets/assets';

const Logo = () => {
  return (
    <Link to={'/'}>
      <img src={assets.logo} className='w-20' alt='logo' />
    </Link>
  );
};

export default Logo;
