import { useState, useEffect } from 'react';
import { useForm } from 'react-hook-form';
import { DevTool } from '@hookform/devtools';
import useSignIn from 'react-auth-kit/hooks/useSignIn';
import { useDispatch, useSelector } from 'react-redux';

import { useNavigate } from 'react-router-dom';
import { UserCreateDto } from '../types/User';
import { createUserThunk } from '../redux/thunks/userThunks';
import { loginUserThunk } from '../redux/thunks/authThunks';
import { AppDispatch, RootState } from '../redux/store';

const Login = () => {
  const {
    register,
    control,
    handleSubmit,
    formState: { errors },
    reset,
  } = useForm<UserCreateDto>();

  const [currentState, setCurrentState] = useState<'Login' | 'Sign Up'>(
    'Login'
  );
  const dispatch = useDispatch<AppDispatch>();
  const navigate = useNavigate();
  const signIn = useSignIn();

  const { userRole, authenticated, loading, error } = useSelector(
    (state: RootState) => state.authR
  );

  useEffect(() => {
    if (authenticated) {
      const path =
        userRole === 'Admin'
          ? '/dashboard/admin/profile'
          : '/dashboard/user/profile';
      navigate(path, { state: userRole });
    }
  }, [authenticated, userRole, navigate]);

  const onSubmit = async (data: UserCreateDto) => {
    try {
      let userData;
      if (currentState === 'Sign Up') {
        userData = await dispatch(createUserThunk(data)).unwrap();
        const path =
          userRole === 'Admin'
            ? '/dashboard/admin/profile'
            : '/dashboard/user/profile';
        navigate(path);
      } else {
        userData = await dispatch(
          loginUserThunk({ email: data.email, password: data.password })
        ).unwrap();
        if (
          signIn({
            auth: {
              token: userData.token,
              type: 'Bearer',
            },
            userState: {
              name: data.email,
              uid: userData.userId,
            },
          })
        ) {
          // Successfully signed in
        } else {
          throw new Error('Sign-in failed');
        }
        navigate('/');
      }
    } catch (error) {
      console.error('Operation failed', error);
    }
  };

  const toggleFormState = () => {
    setCurrentState((prev) => (prev === 'Login' ? 'Sign Up' : 'Login'));
    reset();
  };

  const InputField = ({
    type,
    placeholder,
    name,
    validation,
  }: {
    type: string;
    placeholder: string;
    name: keyof UserCreateDto;
    validation?: Record<string, unknown>;
  }) => (
    <div className='flex items-center w-full'>
      <input
        type={type}
        className='w-full px-3 py-2 border border-gray-800'
        placeholder={placeholder}
        {...register(name, validation)}
      />
      {errors[name] && (
        <span className='ml-2 text-red-500 w-full'>
          {errors[name]?.message as string}
        </span>
      )}
    </div>
  );

  if (loading) return <p className='text-center py-12'>Loading...</p>;
  if (error) return <p className='text-red-500'>{error}</p>;

  return (
    <div>
      <form
        onSubmit={handleSubmit(onSubmit)}
        className='flex flex-col items-center w-full sm:max-w-96 m-auto mt-14 gap-4 text-gray-800'
      >
        <div className='inline-flex items-center gap-2 mb-2 mt-10'>
          <p className='prata-regular text-3xl'>{currentState}</p>
          <hr className='border-none h-[1.5px] w-8 bg-gray-800' />
        </div>

        {currentState === 'Sign Up' && (
          <>
            <InputField
              type='text'
              placeholder='First Name'
              name='firstName'
              validation={{
                required: { value: true, message: 'First Name is required' },
                minLength: {
                  value: 2,
                  message: 'Must be at least 2 characters',
                },
              }}
            />
            <InputField
              type='text'
              placeholder='Last Name'
              name='lastName'
              validation={{
                required: { value: true, message: 'Last Name is required' },
                minLength: {
                  value: 2,
                  message: 'Must be at least 2 characters',
                },
              }}
            />
          </>
        )}

        <InputField
          type='email'
          placeholder='Email'
          name='email'
          validation={{
            required: { value: true, message: 'Email is required' },
            pattern: {
              value: /^[\w-.]+@([\w-]+\.)+[\w-]{2,4}$/g,
              message: 'Email is not valid',
            },
          }}
        />
        <InputField
          type='password'
          placeholder='Password'
          name='password'
          validation={{
            required: { value: true, message: 'Password is required' },
            pattern: {
              value: /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$/,
              message: 'Must be 8 characters, contain letters and numbers',
            },
          }}
        />

        <div className='w-full flex justify-between text-sm mt-[-8px]'>
          <p className='cursor-pointer'>Forgot your password?</p>
          <p onClick={toggleFormState} className='cursor-pointer'>
            {currentState === 'Login' ? 'Create Account' : 'Login Here'}
          </p>
        </div>
        <button
          type='submit'
          className='bg-black text-white px-8 py-3 text-sm active:bg-gray-700 rounded'
        >
          {currentState === 'Login' ? 'Sign In' : 'Sign Up'}
        </button>
      </form>
      <DevTool control={control} />
    </div>
  );
};

export default Login;
