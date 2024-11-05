import { SortType } from '../types/Sort';

const Sort = (props: SortType) => {
  return (
    <div className='sort border h-16 p-4 m-3 text-sm'>
      <label htmlFor='sortBy' className=' mr-3'>
        Sort By
      </label>
      <select value={props.sortCriteria} onChange={props.onHandleSortChange}>
        <option value=''>all</option>
        <option value='title-asc'>A-Z</option>
        <option value='title-desc'>Z-A</option>
        <option value='price-asc'>Lowest Price</option>
        <option value='price-desc'>Highest Price</option>
        <option value='rate-desc'>Highest Rating</option>
      </select>
    </div>
  );
};

export default Sort;
