<?php

use App\Http\Controllers\AuthController;
use App\Http\Controllers\BankController;
use Illuminate\Support\Facades\Route;

// Public routes
Route::post('/register', [AuthController::class, 'register']);
Route::post('/login', [AuthController::class, 'login']);

// Protected routes (Requires authentication)
Route::middleware('auth:sanctum')->group(function () {
    Route::get('/user', [AuthController::class, 'user']);
    Route::get('/account', [BankController::class, 'getAccount']);
    Route::post('/send-money', [BankController::class, 'sendMoney']);
    Route::post('/receive-money', [BankController::class, 'receiveMoney']);
    Route::get('/transactions', [BankController::class, 'getTransactions']);
});
