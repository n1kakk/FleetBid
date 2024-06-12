import React from 'react'
import AuctionCard from './AuctionCard';

async function GetData(){
    const res = await fetch('http://localhost:6001/search?pageSize=10');

    if(!res.ok) throw new Error('Failed to fetch data for searching');

    return res.json();
}

export default async function Listings() {
  const data = await GetData();

  return (
    <div className='grid grid-cols-4 gap-6'>
        {/* {JSON.stringify(data, null, 2)} */}
        {data && data.results.map((auction: any) => (
            <AuctionCard auction={auction} key={auction.id}/>
        ))}
    </div>
  )
}
