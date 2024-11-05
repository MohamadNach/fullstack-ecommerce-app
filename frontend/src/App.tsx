import { Route, Routes } from 'react-router-dom';
import AuthOutlet from '@auth-kit/react-router/AuthOutlet';

import Home from './pages/Home';
import Collection from './pages/Collection';
import About from './pages/About';
import Cart from './pages/Cart';
import Login from './pages/Login';
import Orders from './pages/Orders';
import Product from './pages/Product';
import Contact from './pages/Contact';
import Navbar from './layout/Navbar';
import Checkout from './pages/Checkout';
import NotFound from './pages/NotFound';
import Footer from './layout/Footer';
import AdminRoutes from './routss/AdminRoutes';
import AdminProfile from './pages/AdminProfile';
import UserProfile from './pages/UserProfile';
// import ProtectedRoutes from './routss/ProtectedRoutes';

const App = () => {
  return (
    <div className='px-4 sm:px-[5vw] md:px-[7vw] lg:px-[9vw]'>
      <Navbar />
      <Routes>
        <Route path='/' element={<Home />} />
        <Route path='/contact' element={<Contact />} />
        <Route path='/products/:id' element={<Product />} />
        <Route path='/login' element={<Login />} />
        <Route path='/collection' element={<Collection />} />
        <Route path='/about' element={<About />} />
        <Route path='/cart' element={<Cart />} />
        <Route path='/checkout' element={<Checkout />} />

        {/* Protected routes for user dashboard */}
        <Route element={<AuthOutlet fallbackPath='/login' />}>
          <Route path='profile' element={<UserProfile />} />
          <Route path='orders' element={<Orders />} />
        </Route>

        {/* Protected routes for admin dashboard */}
        <Route path='/dashboard/admin' element={<AdminRoutes />}>
          <Route path='profile' element={<AdminProfile />} />
        </Route>

        {/* Catch-all route for 404 */}
        <Route path='*' element={<NotFound />} />
      </Routes>
      <Footer />
    </div>
  );
};

export default App;
