<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use App\Models\Transaction;

class BankController extends Controller
{
    public function getAccount(Request $request)
    {
        $user = $request->user();
        return response()->json(['balance' => $user->balance], 200);
    }

    public function sendMoney(Request $request)
    {
        $validated = $request->validate([
            'recipient' => 'required|string',
            'account_number' => 'required|string',
            'amount' => 'required|numeric|min:1',
        ]);

        $user = $request->user();

        if ($user->balance < $validated['amount']) {
            return response()->json(['message' => 'Insufficient funds'], 400);
        }

        $user->balance -= $validated['amount'];
        $user->save();

        Transaction::create([
            'user_id' => $user->id,
            'type' => 'Send',
            'recipient' => $validated['recipient'],
            'account_number' => $validated['account_number'],
            'amount' => $validated['amount'],
        ]);

        return response()->json(['message' => 'Money sent successfully'], 200);
    }

    public function receiveMoney(Request $request)
    {
        $validated = $request->validate([
            'sender' => 'required|string',
            'account_number' => 'required|string',
            'amount' => 'required|numeric|min:1',
        ]);

        $user = $request->user();
        $user->balance += $validated['amount'];
        $user->save();

        Transaction::create([
            'user_id' => $user->id,
            'type' => 'Receive',
            'sender' => $validated['sender'],
            'account_number' => $validated['account_number'],
            'amount' => $validated['amount'],
        ]);

        return response()->json(['message' => 'Money received successfully'], 200);
    }

    public function getTransactions(Request $request)
    {
        $transactions = $request->user()->transactions;
        return response()->json($transactions, 200);
    }
}
