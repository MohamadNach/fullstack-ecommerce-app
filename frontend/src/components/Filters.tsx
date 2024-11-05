import { ChangeEvent, useState, useEffect } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { AppDispatch, RootState } from '../redux/store';
import { fetchAllCategoriesThunk } from '../redux/thunks/categoryThunks';
import { CategoryReadDto } from '../types/Category';

const Filters = () => {
  const [showFilter, setShowFilter] = useState(true);
  const [category, setCategory] = useState<string[]>([]);
  const [subCategory, setSubCategory] = useState<string[]>([]);
  const [selectedCategory, setSelectedCategory] = useState<string | null>(null);

  const dispatch = useDispatch<AppDispatch>();
  const { categories, loading, error } = useSelector(
    (state: RootState) => state.categoryR
  );

  useEffect(() => {
    dispatch(fetchAllCategoriesThunk());
  }, [dispatch]);

  const handleCategoryFilter = (e: ChangeEvent<HTMLInputElement>) => {
    const { value } = e.target;

    // Update selected category when a category checkbox is clicked
    if (category.includes(value)) {
      setCategory((prev) => prev.filter((item) => item !== value));
      setSelectedCategory(null); // Deselect when removing
    } else {
      setCategory((prev) => [...prev, value]);
      setSelectedCategory(value); // Set selected category
    }
  };

  const handleSubCategoryFilter = (e: ChangeEvent<HTMLInputElement>) => {
    const { value } = e.target;
    if (subCategory.includes(value)) {
      setSubCategory((prev) => prev.filter((item) => item !== value));
    } else {
      setSubCategory((prev) => [...prev, value]);
    }
  };

  if (loading) {
    return <p>Loading categories...</p>;
  }

  if (error) {
    return <p>Error loading categories: {error}</p>;
  }

  return (
    <div className='flex flex-col sm:flex-row gap-1 sm:gap-10 pt-10'>
      <div className='min-w-60 py-8'>
        <p
          onClick={() => setShowFilter(!showFilter)}
          className='my-2 text-xl flex items-center cursor-pointer gap-2'
        >
          FILTERS
        </p>
        {/* Category Filter */}
        <div
          className={`border border-gray-300 pl-5 py-3 mt-6 ${
            showFilter ? '' : 'hidden'
          }`}
        >
          <p className='mb-3 text-sm font-medium'>CATEGORIES</p>
          <div className='flex flex-col gap-2 text-sm font-light text-gray-700'>
            {categories.map((cat: CategoryReadDto) => (
              <div key={cat.id}>
                <p className='flex gap-2'>
                  <input
                    className='w-3'
                    type='checkbox'
                    value={cat.categoryName}
                    onChange={handleCategoryFilter}
                  />{' '}
                  {cat.categoryName}
                </p>
                {/* Render Subcategories */}
                {selectedCategory === cat.categoryName &&
                  cat.subCategories.length > 0 && (
                    <div className='ml-4'>
                      {cat.subCategories.map((subCat) => (
                        <p className='flex gap-2' key={subCat.id}>
                          <input
                            className='w-3'
                            type='checkbox'
                            value={subCat.categoryName}
                            onChange={handleSubCategoryFilter}
                          />{' '}
                          {subCat.categoryName}
                        </p>
                      ))}
                    </div>
                  )}
              </div>
            ))}
          </div>
        </div>
      </div>
    </div>
  );
};

export default Filters;
