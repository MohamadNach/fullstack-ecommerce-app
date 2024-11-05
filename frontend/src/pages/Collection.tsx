import { ChangeEvent, useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { Pagination } from '@mui/material';

import { AppDispatch, RootState } from '../redux/store';
import ProductItem from '../components/ProductItem';
import Filters from '../components/Filters';
import Title from '../components/Title';
import Sort from '../components/Sort';
import Search from '../components/Search';
import { ProductReadDto } from '../types/Product';
import { fetchAllProductThunk } from '../redux/thunks/productThunks';

const Collection = () => {
  const itemsPerPage = 5;
  const dispatch: AppDispatch = useDispatch();

  const {
    products = [],
    loading,
    error,
    totalPages,
  } = useSelector((state: RootState) => state.productR);

  const [sortCriteria, setSortCriteria] = useState<string>('');
  const [searchValue, setSearchValue] = useState<string>('');
  const [currentPage, setCurrentPage] = useState<number>(1);

  // Sorting handler
  const handleSortingChange = (e: ChangeEvent<HTMLSelectElement>) => {
    setSortCriteria(e.target.value);
    setCurrentPage(1); // Reset to the first page when sorting changes
  };

  // Search handler
  const handleSearchChange = (e: ChangeEvent<HTMLInputElement>) => {
    setSearchValue(e.target.value);
    setCurrentPage(1); // Reset to the first page when search changes
  };

  // Fetch products when page, sort criteria, or search value changes
  useEffect(() => {
    const fetchProducts = async () => {
      try {
        await dispatch(
          fetchAllProductThunk({
            page: currentPage,
            perPage: itemsPerPage,
          })
        );
      } catch (err) {
        console.error('Failed to fetch products', err);
      }
    };

    fetchProducts();
  }, [currentPage, sortCriteria, searchValue, dispatch]);

  // Pagination handler
  const handlePageChange = (_event: ChangeEvent<unknown>, page: number) => {
    setCurrentPage(page);
  };

  if (loading) return <p className='text-center py-12'>Loading...</p>;
  if (error) return <p className='text-red-500'>{error}</p>;

  return (
    <div className='flex flex-col sm:flex-row gap-1 sm:gap-10 pt-10 border-t'>
      <Filters />

      <div className='flex flex-col gap-1 sm:gap-10 text-center py-8 text-xl'>
        <div className='grid grid-cols-1 sm:grid-cols-2 md:grid-cols-2 lg:grid-cols-2 gap-y-6 border'>
          <Sort
            sortCriteria={sortCriteria}
            onHandleSortChange={handleSortingChange}
          />
          <Search
            searchValue={searchValue}
            onHandleSearchChange={handleSearchChange}
          />
        </div>
        <Title text1='ALL' text2='PRODUCTS' />

        <div className='grid grid-cols-2 sm:grid-cols-3 md:grid-cols-4 lg:grid-cols-5 gap-4 gap-y-6'>
          {products.length > 0 ? (
            products.map((item: ProductReadDto) => (
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
            ))
          ) : (
            <p>No products found</p>
          )}
        </div>

        <Pagination
          className='mx-auto'
          count={totalPages}
          page={currentPage}
          onChange={handlePageChange}
          variant='outlined'
          shape='circular'
        />
      </div>
    </div>
  );
};

export default Collection;
