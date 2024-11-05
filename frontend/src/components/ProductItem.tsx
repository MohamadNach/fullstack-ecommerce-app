import React from 'react';
import { Link } from 'react-router-dom';
import { assets } from '../assets/assets';
import { ProductReadDto } from '../types/Product';
import { useDispatch, useSelector } from 'react-redux';
import { addToCart } from '../redux/slices/cartSlice';
import { RootState } from '../redux/store';

const ProductItem: React.FC<ProductReadDto> = ({
  id,
  productTitle,
  description,
  price,
  quantity,
  brandName,
}) => {
  const { images, image, loading, error } = useSelector(
    (state: RootState) => state.imageR
  );
  const dispatch = useDispatch();

  const HandleAddToCart = () => {
    const product = {
      product: productTitle,
      quantity: 1,
      unitPrice: price,
      totalPrice: price,
      size: '',
      color: '',
      createdAt: new Date().toISOString(),
    };
    dispatch(addToCart(product));
  };
  console.log(images);
  console.log(image);
  if (loading) {
    return <p>Loading images...</p>;
  }

  if (error) {
    return <p>Error loading images: {error}</p>;
  }

  return (
    <div>
      <Link className='text-gray-700 cursor-pointer' to={`/products/${id}`}>
        <div className='overflow-hidden'>
          <img
            className='hover:scale-110 transition ease-in-out rounded-lg'
            src={assets.default_image}
            alt={productTitle}
          />
        </div>
        <p className='pt-3 pb-1 text-sm'>{productTitle}</p>
        <p className='pt-3 pb-1 text-sm'>{description}</p>
        <p className='pt-3 pb-1 text-sm'>${price.toFixed(2)}</p>
        <p className='text-sm font-medium'>In Stock: {quantity}</p>
        <p className='text-sm font-medium'>{brandName}</p>
      </Link>
      <button
        onClick={HandleAddToCart}
        className='bg-black text-white px-3 py-2 text-sm active:bg-gray-500 rounded-md'
      >
        ADD TO CART
      </button>
    </div>
  );
};

export default ProductItem;
