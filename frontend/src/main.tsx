import { createRoot } from 'react-dom/client';
import AuthProvider from 'react-auth-kit';
import { BrowserRouter } from 'react-router-dom';
import { Provider } from 'react-redux';
import store from './redux/store';

import App from './App.tsx';
import './index.css';
import { authStore } from './redux/slices/authSlice.ts';

createRoot(document.getElementById('root')!).render(
  <AuthProvider store={authStore}>
    <BrowserRouter>
      <Provider store={store}>
        <App />
      </Provider>
    </BrowserRouter>
  </AuthProvider>
);
