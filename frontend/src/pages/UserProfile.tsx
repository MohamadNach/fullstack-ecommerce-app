import { useLocation } from 'react-router-dom';
import Title from '../components/Title';

const UserProfile = () => {
  const { state } = useLocation();
  return (
    <div>
      <Title text1={'USER'} text2={'PROFILE'} />
      {state ? (
        <article>
          <p>User Name: {state.name}</p>
          <p>User Email: {state.email}</p>
          <p>User Phone: {state.phone}</p>
          <p>User Country: {state.country}</p>
          <p>User City: {state.city}</p>
        </article>
      ) : (
        <p>No user date available</p>
      )}
    </div>
  );
};

export default UserProfile;
