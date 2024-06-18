'use client'

import { useAuctionStore } from '@/hooks/useAuctionStore';
import { useBidStore } from '@/hooks/useBidStore';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr'
import React, { ReactNode, useEffect, useState } from 'react'
import { Bid } from '../types';

type Props = {
    children: React.ReactNode
}

export default function SignalRProvider({children}: Props) {

    const[connection, setConnection] = useState<HubConnection | null>(null);
    const setCurrentPrice = useAuctionStore(state => state.setCurrentPrice);
    const addBid = useBidStore(state => state.addBid);

    useEffect(() => {
        const newConnection = new HubConnectionBuilder()
            .withUrl('http://localhost:6001/notifications',{  
                withCredentials: false,
            
            })
            .withAutomaticReconnect()
            .build();

        setConnection(newConnection);
    }, [])

    useEffect(() => {
        if(connection){
            connection.start()
                .then(() => {
                    console.log('Connected to nt hub');

                    connection.on('BidPlaced', (bid: Bid) => {
                        console.log('Bid placed event received');
                        if(bid.bidStatus.includes('Accepted')){
                            setCurrentPrice(bid.auctionId, bid.amount);
                        }
                    })
                }).catch(error => console.log(error));
        }

        return () => {
            connection?.stop();
        }
    }, [connection, setCurrentPrice])

  return (
    children
  )
}
