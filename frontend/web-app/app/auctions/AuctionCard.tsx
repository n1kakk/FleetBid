import Image from 'next/image'
import React from 'react'

type Props = {
    auction: any
    // auction: {
    //     imageUrl: string;
    //     make: string;
    //   };
};

export default function AuctionCard({auction}: Props) {

    //const placeholderImage = "https://via.placeholder.com/1200x720";
    //const imageUrl = auction.imageUrl || placeholderImage;
    //const imageUrl = "https://cdn.pixabay.com/photo/2012/11/02/13/02/car-63930_960_720.jpg";

  return (
    // <div>{auction.make}</div>
    <a href='#'>
        <div className='w-full bg-gray-200 aspect-video rounded-lg overflow-hidden'>
            <div>
            <img
                src={auction.imageUrl}
                alt='image'
                className='object-cover'
                width={1200}
                height={720}
                sizes='(max-width:768px) 100vw, (max-width: 1200px) 50vw, 25vw'

                // quality={75}
            />
            </div>

        </div>
        <div className='flex justify-between items-center mt-4'>
            <h3 className='text-gray-700'>{auction.make} {auction.model}</h3>
            <p className='font-semibold text-sm'>{auction.year}</p>

        </div>
    </a>
  )
}
