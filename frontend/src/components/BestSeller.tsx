import { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';

import ProductItem from './ProductItem';
import Title from './Title';
import { AppDispatch, RootState } from '../redux/store';
import { fetchAllProductThunk } from '../redux/thunks/productThunks';
import { ProductReadDto } from '../types/Product';

const BestSeller = () => {
  const itemsPerPage = 5;
  const dispatch: AppDispatch = useDispatch();
  const [latestProducts, setLatestProducts] = useState<ProductReadDto[]>([]);
  const [currentPage, setCurrentPage] = useState<number>(1);

  const {
    products = [],
    loading,
    error,
  } = useSelector((state: RootState) => state.productR);

  useEffect(() => {
    const fetchLatestProducts = async () => {
      try {
        await dispatch(
          fetchAllProductThunk({
            page: currentPage,
            perPage: itemsPerPage,
          })
        );
        setCurrentPage(1);
      } catch (err) {
        console.error('Failed to fetch products', err);
      }
    };

    fetchLatestProducts();
  }, [currentPage, dispatch]);

  // Filter and sort the products to get the latest 5
  useEffect(() => {
    const sortedProducts = [...products].sort(
      (a, b) =>
        new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime()
    );
    setLatestProducts(sortedProducts.slice(0, itemsPerPage));
  }, [products]);

  if (loading) return <p className='text-center py-12'>Loading...</p>;
  if (error) return <p className='text-red-500'>{error}</p>;

  return (
    <div className='my-10'>
      <div className='text-center py-8 text-3xl'>
        <Title text1={'BEST'} text2={'COLLECTION'} />
        {/* <p className='w-3/4 m-auto text-xs sm:text-sm md:text-base text-gray-600'>
          Latest  Description
        </p> */}
        <div className='grid grid-cols-2 sm:grid-cols-3 md:grid-cols-4 lg:grid-cols-5 gap-4 gap-y-6'>
          {latestProducts.map((item: ProductReadDto) => (
            <ProductItem
              key={item.id}
              id={item.id}
              productTitle={item.productTitle}
              price={item.price}
              categoryId={item.categoryId}
              description={item.description}
              brandName={item.brandName}
              quantity={item.quantity}
              createdAt={''}
            />
          ))}
        </div>
      </div>
    </div>
  );
};

export default BestSeller;
