import { useSelector, useDispatch } from 'react-redux';
import { RootState } from '../redux/store';
import { removeFromCart, updateCartItem } from '../redux/slices/cartSlice';
import { CartItemReadDto } from '../types/CartItem';
import CartItem from '../components/CartItem';
import Title from '../components/Title';

const Cart = () => {
  const dispatch = useDispatch();
  const { cartItems, totalPrice, loading, error } = useSelector(
    (state: RootState) => state.cartR
  );

  const handleRemoveItem = (id: string) => {
    dispatch(removeFromCart(id));
  };

  const handleUpdateQuantity = (id: string, quantity: number) => {
    // Ensure the quantity is not less than 1
    if (quantity < 1) return;

    const updatedItem = cartItems.find((item) => item.id === id);
    if (updatedItem) {
      dispatch(updateCartItem({ ...updatedItem, quantity }));
    }
  };

  if (loading) return <p>Loading...</p>;
  if (error) return <p className='text-red-500'>{error}</p>;

  return (
    <div className='cart-container'>
      <div className='text-center py-8 text-3xl'>
        <Title text1={'Your'} text2={'Cart'} />
      </div>

      {cartItems.length === 0 ? (
        <p>Your cart is empty</p>
      ) : (
        <>
          <ul>
            <div className='grid grid-cols-2 sm:grid-cols-3 md:grid-cols-4 lg:grid-cols-5 gap-4 gap-y-6'>
              {cartItems.map((item: CartItemReadDto) => (
                <li key={item.id}>
                  <CartItem
                    id={item.id}
                    productTitle={item.product}
                    price={item.unitPrice}
                    categoryId={''}
                    description={item.size}
                    brandName={item.color}
                    quantity={item.quantity}
                    createdAt={''}
                  />
                  <div>
                    <p>{item.product}</p>
                    <p>Price: ${item.unitPrice}</p>
                    <p>Quantity: {item.quantity}</p>
                    <button
                      onClick={() => handleRemoveItem(item.id)}
                      className='bg-black text-white px-3 py-1 mx-1 mt-2 text-xs active:bg-gray-500 rounded-md'
                    >
                      Remove
                    </button>
                    <button
                      onClick={() =>
                        handleUpdateQuantity(item.id, item.quantity + 1)
                      }
                      className='bg-black text-white px-3 py-1 mx-2 mt-2 text-xs active:bg-gray-500 rounded-md'
                    >
                      +
                    </button>
                    <button
                      onClick={() =>
                        handleUpdateQuantity(item.id, item.quantity - 1)
                      }
                      className='bg-black text-white px-3 py-1 mx-2 mt-2 text-xs active:bg-gray-500 rounded-md'
                    >
                      -
                    </button>
                  </div>
                </li>
              ))}
            </div>
          </ul>
          <div>
            <h3>Total Price: ${totalPrice.toFixed(2)}</h3>
          </div>
        </>
      )}
    </div>
  );
};

export default Cart;
