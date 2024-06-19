'use client';

//import { getBidsForAuctions } from '@/app/actions/AuthActions'
import Heading from '@/app/components/Heading'
import { Auction, Bid } from '@/app/types'
import { useBidStore } from '@/hooks/useBidStore'
import { User } from 'next-auth'
import React, { useEffect, useState } from 'react'
import toast from 'react-hot-toast'
import BIdItem from './BIdItem'
import { numberWithComma } from '@/app/lib/numberWithComma';
import EmptyFilter from '@/app/components/EmptyFilter';
import BIdForm from './BIdForm';
import { getBidsForAuctions } from '@/app/actions/AuctionActions';

type Props = {
    user: User | null,
    auction: Auction
}

export default function BidList({user, auction}: Props) {
    const[loading, setLoading] = useState(true);
    const bids = useBidStore(state => state.bids);
    const setBids = useBidStore(state => state.setBids);
    const open = useBidStore(state => state.open);
    const setOpen = useBidStore(state => state.setOpen);
    const openForBids = new Date(auction.auctionEnd) > new Date();

    const highBid = bids.reduce((prev, current) => prev > current.amount ? prev : current.bidStatus.includes('Accepted') 
        ? current.amount: prev, 0)

    useEffect(() =>{
        getBidsForAuctions(auction.id)
            .then((res: any) =>{
                if(res.status && res.status !== 200){
                    throw res.message;
                    
                }
                setBids(res as Bid[]);
            }).catch(err =>{
                toast.error(err.message);
            }).finally(() => setLoading(false));
    }, [auction.id, setBids, setLoading])

    useEffect(()=>{
        setOpen(openForBids);
    }, [openForBids, setOpen]);

    if(loading) return <span>Loading bids...</span>

  return (
    <div className='rounded-lg shadow-md'>
        <div className='py-2 px-4 bg-white'>
            <div className='sticky top-0 bg-white p-2'>
                <Heading title={`Current high bid is $${numberWithComma(highBid)}`}/>  
            </div>

            <div className='overflow-auto h-[400px] flex flex-col-reverse px-2'>
                {bids.length === 0 ? (
                    <EmptyFilter title='NO bids for this itme' subtitle='Feel free to make a bid' />
                ) : (  
                  <>
                    {bids.map(bid =>(
                    <BIdItem key={bid.id} bid={bid}/>
                    ))} 
                  </>
                )}
            </div>

            <div className='px-2 pb-2 text-gray-500'>
                {!open ? (
                    <div className='flex items-center justify-center p-2 text-lg font-semibold'>
                        This auction has finished
                    </div>
                ):
                !user ? (
                    <div className='flex items-center justify-center p-2 text-lg font-semibold'>
                        Please login to make a bid
                    </div>
                ): user && user.username===auction.seller ?(
                    <div className='flex items-center justify-center p-2 text-lg font-semibold'>
                        You cannot bid on your own auction
                    </div>
                ): (
                    <BIdForm auctionId={auction.id} highBid={highBid} />
                )}
            </div>

        </div> 
      
  </div> 
  )
}
