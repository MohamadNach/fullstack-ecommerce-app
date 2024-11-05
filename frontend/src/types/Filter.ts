import { ChangeEvent } from 'react';

export type FilterType = {
  filterValue: string;
  onHandleFilterChange: (e: ChangeEvent<HTMLInputElement>) => void;
};
