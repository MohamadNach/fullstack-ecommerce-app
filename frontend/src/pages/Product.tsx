// import React from 'react';

const Product = () => {
  return <div>Product page</div>;
};

export default Product;

// import { useDispatch, useSelector } from 'react-redux';
// import { useNavigate, useParams } from 'react-router-dom';
// // import { fetchSingleProductData } from '../redux/slices/productSlice';
// import { useEffect, useState } from 'react';
// import { AppDispatch, RootState } from '../redux/store';
// import { assets } from '../assets/assets';

// const ProductDetail = () => {
//   const navigate = useNavigate();
//   const { productId } = useParams<string>();
//   const dispatch: AppDispatch = useDispatch();
//   const { product, loading, error } = useSelector(
//     (state: RootState) => state.productR
//   );
//   const [image, setImage] = useState<string>('');
//   const [rating, setRating] = useState<number>(0);

//   // const addToCart = async(itemId, size) => {
//   //   let cartData = structuredClone(cartItems);
//   //   if(cartData[itemid]){
//   //     if(cartData[itemId][size]){
//   //       cartData[itemId][size] += 1;
//   //     }
//   //     else{
//   //       cartData[itemId][size] = 1,
//   //     }
//   //   }
//   // }

//   useEffect(() => {
//     if (productId) {
//       let url = `https://dummyjson.com/products/${productId}`;
//       dispatch(fetchSingleProductData(url));
//     }
//   }, [dispatch, productId]);

//   // Set the first image when the product is fetched
//   useEffect(() => {
//     if (product && product.images.length > 0) {
//       setImage(product.images[0]); // Set the first image as the main image
//       setRating(product.rating); // Set the rating from the product data
//     }
//   }, [product]);
//   if (loading) {
//     return <div>Loading...</div>;
//   }

//   if (error) {
//     return <div>Error: {error}</div>;
//   }
//   const handleBack = () => {
//     navigate('/');
//   };

//   // Helper function to generate an array of stars based on rating
//   const renderStars = (rating: number) => {
//     const totalStars = 5;
//     const filledStars = Math.floor(rating); // Full stars
//     const emptyStars = totalStars - filledStars;

//     return (
//       <>
//         {Array(filledStars)
//           .fill(null)
//           .map((_, index) => (
//             <img
//               key={index}
//               src={assets.star}
//               alt='filled star'
//               className='w-3.5'
//             />
//           ))}
//         {Array(emptyStars)
//           .fill(null)
//           .map((_, index) => (
//             <img
//               key={index}
//               src={assets.star_dull}
//               alt='empty star'
//               className='w-3.5'
//             />
//           ))}
//       </>
//     );
//   };

//   return product ? (
//     <div className='border-t-2 pt-10 transition-opacity ease-in duration-500 opacity-100'>
//       <button
//         onClick={handleBack}
//         className=' bg-black text-white px-5 py-3 text-sm active:bg-gray-700 rounded'
//       >
//         Back
//       </button>
//       <div className='flex gap-12 sm:gap-12 flex-col sm:flex-row'>
//         <div className='flex-1 flex flex-col-reverse gap-3 sm:flex-row'>
//           <div className='flex sm:flex-col overflow-x-auto sm:overflow-y-scroll justify-between sm:justify-normal sm:w-[18.7%] w-full'>
//             {product.images.map((item: string, index: number) => (
//               <img
//                 onClick={() => setImage(item)}
//                 src={item}
//                 key={index}
//                 className='w-[24%] sm:w-[80%] sm:mb-3 flex-shrink-0 cursor-pointer'
//                 alt=''
//               />
//             ))}
//           </div>
//           <div className='w-full sm:w-[50%] '>
//             {/* Should put default image */}
//             <img src={image} className='w-[80%] h-auto' alt='' />
//           </div>
//           {
//             <div className='flex-1'>
//               <h1 className='font-medium text-2xl mt-2'>{product.title}</h1>
//               {/* Dynamic star rating */}
//               <div className='flex items-center gap-1 mt-2'>
//                 {renderStars(rating)}
//                 <p className='pl-2'>({rating.toFixed(1)})</p>
//               </div>
//               <p className='mt-5 text-3xl font-medium'>€ {product.price}</p>
//               <p className='mt-5 text-gray-500 md:w-4/5'>
//                 € {product.description}
//               </p>
//               <div className='flex flex-col gap-4 my-8'>
//                 <p>Select Size</p>
//                 {/* For size */}
//                 {/* <div className='flex gap-2'>
//                   {
//                     product.size.map((item, index)=>(
//                       <button  key={index}>{item}</button>
//                     ))
//                   }
//                 </div> */}
//               </div>
//               <div className='flex flex-col gap-4 my-8'>
//                 <p>Select Color</p>
//                 {/* For color */}
//                 {/* <div className='flex gap-2'>
//                   {
//                     product.color.map((item, index)=>(
//                       <button key={index}>{item}</button>
//                     ))
//                   }
//                 </div> */}
//               </div>
//               <button className='bg-black text-white px-8 py-3 text-sm active:bg-gray-700 rounded'>
//                 ADD TO CART
//               </button>
//               <hr className='mt-8 sm:w-4/5' />
//               <div className='text-sm tex-gray-500 mt-5 flex flex-col gap-1'>
//                 <p>100% Original Product.</p>
//               </div>
//             </div>
//           }
//         </div>
//       </div>
//       <div className='mt-20'>
//         <div className='flex'>
//           <b className='border px-5 py-3 text-sm'>Warranty</b>
//           <p className='border px-5 py-3 text-sm'>Reviews (122)</p>
//         </div>
//         <div className='flex flex-col gap-4 border px-6 py-6 text-sm text-gray-500'>
//           <p>{product.warrantyInformation}</p>
//         </div>
//       </div>
//     </div>
//   ) : (
//     <div className='opacity-0'></div>
//   );
// };

// export default ProductDetail;
