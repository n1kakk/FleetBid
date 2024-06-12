import React from 'react'
import { IoCarSportSharp } from "react-icons/io5";

export default function Navbar() {
    return (
        <header className='
        sticky top-0 z-50 flex justify-between bg-white p-5 items-center text-gray-800 shadow-md
        '>
           <div className='flex items-center gap-2 text-3xl text-red-500'>
                <IoCarSportSharp size={34}/>
                <div>FleetBid Auctions</div>
            </div>
           <div>Search</div>
           <div>Login</div>
        </header>
    )
}