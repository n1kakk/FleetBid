'use client'

import { placeBidForAuction } from '@/app/actions/AuctionActions'
import { numberWithComma } from '@/app/lib/numberWithComma'
import { useBidStore } from '@/hooks/useBidStore'
import React from 'react'
import { FieldValues, useForm } from 'react-hook-form'
import toast from 'react-hot-toast'

type Props ={
    auctionId: string,
    highBid: number
}

export default function BIdForm({auctionId, highBid}: Props) {
    const {register, handleSubmit, reset, formState:{errors}} = useForm();
    const addBid = useBidStore(state => state.addBid);

    function onSubmit(data: FieldValues){
        if(data.amount <= highBid){
            reset();
            return toast.error('BId must be at least $' + numberWithComma(highBid + 1));
        } 
        placeBidForAuction(auctionId, +data.amount).then(bid =>{
            if (bid.status && bid.status !== 200) { 
                throw new Error(bid.message || 'An error occurred');
            }
            addBid(bid);
            reset();
        }).catch(err => {
            toast.error(err.message || "An error occurred while placing the bid.");
        });
    }

  return (
    <form onSubmit={handleSubmit(onSubmit)} className='flex items-center border-2 rounded-lg py-2'>
        <input type='number' 
        {...register('amount')}
        className='input-custom text-smtext-gray-600'
        placeholder={`Enter your bids (minimum bid is $${numberWithComma(highBid + 1)})`}
        />
    </form>
  )
}
