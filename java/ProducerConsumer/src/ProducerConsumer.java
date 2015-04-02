
import java.util.LinkedList;
import java.util.Queue;
import java.util.Timer;
import java.util.TimerTask;

/**
 *
 * @author Michael Hargiss
 * Course: CS 3670
 * Assignment: Producer/Consumer with threads
 * 
 */

public class ProducerConsumer {
    private static LinkedList<Producer> producerList = new LinkedList<Producer>();
    private static LinkedList<Consumer> consumerList = new LinkedList<Consumer>();
        
    public static void main( String[] args ) {
        int numProducers = 2;
        int numConsumers = 1;
        
        System.out.println( "Starting ProducerConsumer program..." );
        
        try {
            // create IOQueue instance
            IOQueue queue = new IOQueue();

            // create Consumer and Producer threads
            while( numProducers != producerList.size() ) {
                producerList.add( new Producer( queue, producerList.size() + 1 ) );
            }
            while( numConsumers != consumerList.size() ) {
                consumerList.add( new Consumer( queue, consumerList.size() + 1 ) );
            }
            
            // start threads
            for( Producer p : producerList ) {
                p.start();
            }
            for( Consumer c : consumerList ) {
                c.start();
            }

            // let threads run for 20s, then kill them
            long twentySeconds = 20 * 1000;
            Timer timer = new Timer();
            timer.schedule( new TimerTask() {
               @Override
               public void run() {
                   try {
                        killThreadsAndEndProgram();
                   } catch ( Exception e ) {
                       System.out.println( "TimerTask error: " + e.getMessage() );
                   }
               }
            }, twentySeconds );
        } catch( Exception e ) {
            System.out.println( "main() error: " + e.getMessage() );
        }
    }
    
    private static void killThreadsAndEndProgram() {
        for ( Producer p : producerList ) {
            if ( p.isAlive() ) {
                System.out.println( "Stopping thread " + p.toString() );
                p.shouldQuit = true;
            }
        }
        
        for ( Consumer c : consumerList ) {
            if ( c.isAlive() ) {
                System.out.println( "Stopping thread " + c.toString() );
                c.shouldQuit = true;
            }
        }
        
        System.out.println( "Ending program..." );
        System.exit(0);
    }
    
    static class IOQueue {
        private static int available;
        private static Queue<Message> IOQueue;
        
        public IOQueue() {
            available = 0;
            IOQueue = new LinkedList<Message>();
        }

        private synchronized Message Read() {
            if ( isAvailable() == 0 ) {
                try {
                    wait();
                } catch ( InterruptedException ie ) {
                    System.out.println( "IOQueue Read() error: " + ie.getMessage() );
                }   
            } else {
                Message message = IOQueue.remove();
                setAvailableFalse();
                notifyAll();
                return message;
            }
            
            return new Message();
        }

        private synchronized boolean Write( Message a_Message ) {
            if ( isAvailable() == 1 ) {
                try {
                    wait();
                } catch ( InterruptedException ie ) {
                    System.out.println( "IOQueue Write() error: " + ie.getMessage() );
                }
            } else {
                IOQueue.add( a_Message );
                setAvailableTrue();
                notifyAll();
                return true;
            }
            
            return false;
        }
        
        public synchronized void setAvailableTrue() {
            available = 1;
        }

        public synchronized void setAvailableFalse() {
            available = 0;
        }

        public synchronized int isAvailable() {
            return available;
        }
    }
    
    static class Producer extends Thread {
        private IOQueue queue;
        private int ID;
        private int messageCount;
        public boolean shouldQuit;
        
        public Producer( IOQueue a_IOQueue, int a_producerNumber ) {
            queue = a_IOQueue;
            ID = a_producerNumber;
            messageCount = 1;
            shouldQuit = false;
        }
        
        @Override
        public void run() {
            while ( !shouldQuit ) {
                try {
                    if ( queue.Write( new Message( messageCount, this.toString() ) ) ) {
                        messageCount += 1;
                        Thread.sleep( 500 );
                    }
                } catch ( Exception e ) {
                    System.out.println( this.toString() + " error: " + e.getMessage() );
                }
            }
        }
        
        @Override
        public String toString() {
            return "Producer" + Integer.toString( this.ID );
        }
    }
    
    static class Consumer extends Thread {
        private IOQueue queue;
        private int ID;
        public boolean shouldQuit;
        
        public Consumer( IOQueue a_IOQueue, int a_consumerNumber ) {
            queue = a_IOQueue;
            ID = a_consumerNumber;
            shouldQuit = false;
        }
        
        @Override
        public void run() {
            while ( !shouldQuit ) {
                try {
                    Message message = queue.Read();
                    if ( message != null && message.ID > 0 ) {
                        System.out.println( this.toString() + " read message " + message.toString() );
                    }
                } catch ( Exception e ) {
                    System.out.println( this.toString() + " error: " + e.getMessage() );
                }
            }
        }
        
        @Override
        public String toString() {
            return "Consumer" + Integer.toString( this.ID );
        }
    }
    
    static class Message {
        private int ID;
        private String threadName;
        
        public Message() {
            ID = 0;
            threadName = "";
        }
        
        public Message( int a_ID, String a_threadName ) {
            ID = a_ID;
            threadName = a_threadName;
        }
        
        @Override
        public String toString() {
            return threadName + "-" + Integer.toString( ID );
        }
    }
}
