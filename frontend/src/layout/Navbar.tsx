import { Link, NavLink, useNavigate } from 'react-router-dom';
import { useState, useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { IoSearch } from 'react-icons/io5';
import { FaCartPlus } from 'react-icons/fa';
import useSignOut from 'react-auth-kit/hooks/useSignOut';
import { CgProfile } from 'react-icons/cg';
import MenuIcon from '@mui/icons-material/Menu';
import { Button, MenuItem, MenuList, Paper } from '@mui/material';
import Logo from '../components/Logo';
import { AppDispatch, RootState } from '../redux/store'; // Ensure correct imports
import { logoutUserThunk } from '../redux/thunks/authThunks';

const Navbar = () => {
  const [visible, setVisible] = useState(false);
  const dispatch = useDispatch<AppDispatch>();
  const navigate = useNavigate();
  const signOut = useSignOut();
  const totalQuantity = useSelector((state: RootState) =>
    state.cartR.cartItems.reduce((total, item) => total + item.quantity, 0)
  );
  const isLoggedIn = useSelector(
    (state: RootState) => state.authR.authenticated
  );
  const deleteCookie = (name: string) => {
    document.cookie = `${name}=; Max-Age=0; path=/;`;
    document.cookie = `${name}_state=; Max-Age=0; path=/;`;
    document.cookie = `${name}_type=; Max-Age=0; path=/;`;
  };

  const handleLogout = () => {
    signOut();
    dispatch(logoutUserThunk());
    deleteCookie('_auth');
    navigate('/');
  };

  useEffect(() => {}, [isLoggedIn]);

  return (
    <div className='flex items-center justify-between py-5 font-medium bg-cyan-50 p-4'>
      <Logo />
      <ul className='sm:flex gap-5 text-sm text-gray-700 hidden'>
        <NavLink className='flex flex-col items-center gap-1' to={'/'}>
          <p>Home</p>
        </NavLink>
        <NavLink
          className='flex flex-col items-center gap-1'
          to={'/collection'}
        >
          <p>Collection</p>
        </NavLink>
        <NavLink className='flex flex-col items-center gap-1' to={'/Contact'}>
          <p>Contact</p>
        </NavLink>
        <NavLink className='flex flex-col items-center gap-1' to={'/about'}>
          <p>About</p>
        </NavLink>
      </ul>

      <div className='flex items-center gap-5'>
        <IoSearch className='w-6 h-6' />

        <div className='group relative'>
          <Link to={'/login'}>
            <CgProfile className='w-6 h-6' />
          </Link>
          {isLoggedIn && (
            <div className='group-hover:block hidden absolute dropdown-menu right-0 pt-4'>
              <div className='flex flex-col gap-2 w-36 py-3 px-5 bg-slate-100 text-gray-700 rounded'>
                <Link to={'/dashboard/userProfile'}>
                  <p className='cursor-pointer hover:text-black '>My Profile</p>
                </Link>
                <Link to={'/orders'}>
                  <p className='cursor-pointer hover:text-black '>Orders</p>
                </Link>
                <Button onClick={handleLogout}>
                  <p className='cursor-pointer hover:text-black '>Logout</p>
                </Button>
              </div>
            </div>
          )}
        </div>

        <Link to='/cart' className='relative'>
          <FaCartPlus className='w-6 h-6' />
          {totalQuantity > 0 && (
            <span className='absolute top-3 left-3 bg-red-600 text-white text-xs rounded-full w-4 h-4 flex items-center justify-center'>
              {totalQuantity}
            </span>
          )}
        </Link>
        <div
          onClick={() => setVisible(true)}
          className='w-5 cursor-pointer sm:hidden'
        >
          <MenuIcon />
        </div>
      </div>

      <div
        className={`absolute top-0 right-0 bottom-0 overflow-hidden bg-white transition-all ${
          visible ? 'w-full' : 'w-0'
        }`}
      >
        <Paper>
          <MenuList>
            {['Home', 'Collection', 'Contact', 'About'].map((item) => (
              <MenuItem
                key={item}
                onClick={() => setVisible(false)}
                component={Link}
                to={`/${item.toLowerCase()}`}
                sx={{
                  backgroundColor: 'inherit',
                  '&:hover': {
                    backgroundColor: 'lightblue',
                  },
                  padding: '10px',
                }}
              >
                {item}
              </MenuItem>
            ))}
          </MenuList>
        </Paper>
      </div>
    </div>
  );
};

export default Navbar;
