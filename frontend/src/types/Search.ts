import { ChangeEvent } from 'react';

export type SearchType = {
  searchValue: string;
  onHandleSearchChange: (e: ChangeEvent<HTMLInputElement>) => void;
};
