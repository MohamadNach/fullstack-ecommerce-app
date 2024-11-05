import { assets } from '../assets/assets';

const Footer = () => {
  return (
    <div>
      <div className='flex flex-col sm:grid grid-cols-[3fr_1fr_1fr] gap-14 my-10 mt-40 text-sm'>
        <div>
          <img src={assets.logo} className='mb-5 w-20' alt='' />
          <p className='w-full md:w-2/3 text-gray-600'>We tyr our best</p>
        </div>
        <div>
          <p className='text-xl font-medium mb-5'>COMPANY</p>
          <ul className='flex flex-col gap-1 text-gray-600'>
            <li>Home</li>
            <li>About us</li>
            <li>Delivery</li>
            <li>Privasy policy</li>
          </ul>
        </div>
        <div>
          <p className='text-xl font-medium mb-5'>CONTACT US</p>
          <ul className='flex flex-col gap-1 text-gray-600'>
            <li>+358-333-33-33</li>
            <li>contact@ecommerce.com</li>
          </ul>
        </div>
      </div>
      <div>
        <hr />
        <p className='py-5 text-sm text-center'>Copyright 2024</p>
      </div>
    </div>
  );
};

export default Footer;
