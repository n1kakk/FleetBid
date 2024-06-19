import React from 'react'
import { Auction } from '../types'
import Link from 'next/link'

type Props = {
    auction: Auction
}

export default function AuctionCreatedToast({auction}: Props) {
  return (
    <Link href={`/auctions/details/${auction.id}`} className='flex flex-col items-center'>
        <div className='flex flex-row items-center gap-2'>
            <img
            src={auction.imageUrl}
            alt='img'
            height={80}
            width={80}
            className='rounded-lg w-auto h-auto'
            />

            <span>New auction!! {auction.make} {auction.model} has been added</span>
        </div>
    </Link>
  )
}
