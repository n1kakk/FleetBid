'use client'

import React from 'react'
import Countdown from 'react-countdown';
//import zeroPad from 'zeropad';

type Props ={
    auctionEnd: string
}

const renderer = ({days, hours, minutes, seconds, completed }:
    {days: number, hours: number, minutes: number, seconds: number, completed: boolean}
) => {
    var zeroPad = require('zeropad');
    return (
    
        <div className={`
            border-2 border-white text-white py-1 px-2 rounded-lg flex items-center justify-center text-sm
            ${completed ? 'bg-red-600': (days === 0 && hours < 10) ?
             'bg-amber-600' : 'bg-green-600'
            }
        `}  style={{ width: '80px', height: '27px' }}>
            {completed ? (
                <span>Auction finished</span>
            ): (<span suppressHydrationWarning={true}>{zeroPad(days)}:{zeroPad(hours)}:{zeroPad(minutes)}:{zeroPad(seconds)}</span>)}
        </div>
    )
  };

function CountdownTimer({auctionEnd}: Props) {
  return (
    <div>
        <Countdown date={auctionEnd} renderer={renderer}/>
    </div>
  )
}

export default CountdownTimer