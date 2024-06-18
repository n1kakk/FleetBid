'use client';

import { getBidsForAuctions } from '@/app/actions/AuthActions'
import Heading from '@/app/components/Heading'
import { Auction, Bid } from '@/app/types'
import { useBidStore } from '@/hooks/useBidStore'
import { User } from 'next-auth'
import React, { useEffect, useState } from 'react'
import toast from 'react-hot-toast'
import BIdItem from './BIdItem'

type Props = {
    user: User | null,
    auction: Auction
}

export default function BidList({user, auction}: Props) {
    const[loading, setLoading] = useState(true);
    const bids = useBidStore(state => state.bids);
    const setBids = useBidStore(state => state.setBids);

    useEffect(() =>{
        getBidsForAuctions(auction.id)
            .then((res: any) =>{
                if(res.error){
                    throw res.error;
                    
                }
                setBids(res as Bid[]);
            }).catch(err =>{
                toast.error(err.message);
            }).finally(() => setLoading(false));
    }, [auction.id, setBids, setLoading])

    if(loading) return <span>Loading bids...</span>

  return (
    <div className='border-2 rounded-lg p-2 bg-gray-100'>
    <Heading title='Bids'/>   
    {bids.map(bid =>(
      <BIdItem key={bid.id} bid={bid}/>
    ))}       
  </div> 
  )
}
