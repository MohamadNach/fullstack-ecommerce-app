import { SearchType } from '../types/Search';

const Search = (props: SearchType) => {
  return (
    <div className='search border h-16 p-4 m-3 text-sm'>
      <label htmlFor='search' className=' mr-3'>
        Search
      </label>
      <input
        type='search'
        id='search'
        name='search'
        value={props.searchValue}
        placeholder='Search...'
        onChange={props.onHandleSearchChange}
      />
    </div>
  );
};

export default Search;
