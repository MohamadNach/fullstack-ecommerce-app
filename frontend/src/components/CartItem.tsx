import React from 'react';
import { Link } from 'react-router-dom';
import { assets } from '../assets/assets';
import { ProductReadDto } from '../types/Product';

const CartItem: React.FC<ProductReadDto> = ({
  id,
  productTitle,
  description,
  price,
  quantity,
  brandName,
}) => {
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
    </div>
  );
};

export default CartItem;
